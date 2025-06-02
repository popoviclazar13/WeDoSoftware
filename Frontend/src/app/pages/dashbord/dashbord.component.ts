import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { JwtHelperService } from '../../services/jwtHelper.service';
import { UserService } from '../../services/user.service';
import { TrainingService } from '../../services/training.service';
import { CommonModule } from '@angular/common';
import { Training } from '../../models/training';
import { ExerciseType } from '../../models/exercise-type';
import { WeeklyStat } from '../../models/weeklyStats';
import { Router } from '@angular/router';

@Component({
  selector: 'app-dashbord',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './dashbord.component.html',
  styleUrl: './dashbord.component.css'
})
export class DashbordComponent implements OnInit {

  loggedInUser: any;

  userId: number | undefined;
  userTrainings: Training[] = [];

  userForm: FormGroup;
  trainingForm: FormGroup;
  
  selectedTraining: Training | null = null;

  selectedMonth: string = '';
  weeklyStats: any[] = [];

  exerciseTypes = [
    { value: ExerciseType.Cardio, label: 'Cardio' },
    { value: ExerciseType.Strength, label: 'Strength' },
    { value: ExerciseType.Flexibility, label: 'Flexibility' }
  ];

  constructor(private fb: FormBuilder,
     private jwtHelper: JwtHelperService,
     private userService: UserService,
     private router: Router,
     private trainingService: TrainingService)
    {
    this.userForm = this.fb.group({
      username: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required]
    });

    this.trainingForm = this.fb.group({
      exerciseType: ['', Validators.required],
      durationInMinutes: ['', Validators.required],
      calories: ['', Validators.required],
      intensity: [1, [Validators.required, Validators.min(1), Validators.max(10)]],
      fatigue: [1, [Validators.required, Validators.min(1), Validators.max(10)]],
      dateTime: [''],
      notes: ['']
    });
  }

  ngOnInit(): void {
    const decoded = this.jwtHelper.getDecodedToken();
    if (decoded) {
      this.userId = Number(decoded.id);
      this.loggedInUser = {
        username: localStorage.getItem('username')
      };
  
      this.userService.getUserById(this.userId).subscribe(user => {
        this.userForm.patchValue({
          username: user.username,
          email: user.email
        });
      });
  
      this.trainingService.getTrainingByUserId(this.userId).subscribe(trainings => {
        this.userTrainings = trainings;
      });
    }
  }

  logout(): void {
    localStorage.removeItem('token'); 
    localStorage.removeItem('userId');
    localStorage.removeItem('email');
    localStorage.removeItem('username');
    this.router.navigate(['/login']);
  }

  onUserSubmit(): void {
    if (this.userForm.valid && this.userId !== undefined) {
      const updatedUser = {
        id: this.userId,
        ...this.userForm.value
      };
  
      this.userService.updateUser(this.userId, updatedUser).subscribe({
        next: () => {
          console.log('User data succesfully updated');
        },
        error: err => {
          console.error('Error while updating user:', err);
        }
      });
    }
  }

  onTreningSubmit(): void {
    if (this.trainingForm.valid && this.userId !== undefined) {
      const treningPodaci = {
        ...this.trainingForm.value,
        userId: this.userId,
        exerciseType: Number(this.trainingForm.value.exerciseType)
      };
  
      if (this.selectedTraining) {
        // Ažuriranje postojećeg treninga
        this.trainingService.updateTraining(this.selectedTraining.id, treningPodaci).subscribe({
          next: (response: string) => {
            console.log('Backend response:', response);
            this.refreshTrainignList();
            this.trainingForm.reset();
            this.selectedTraining = null;
          },
          error: err => {
            console.error('Error while updating training:', err);
          }
        });
  
      } else {
        // Dodavanje novog treninga
        this.trainingService.createTraining(treningPodaci).subscribe({
          next: () => {
            console.log('Training data succesfully created');
            this.refreshTrainignList();
            this.trainingForm.reset();
          },
          error: err => {
            console.error('Error while creating training:', err);
          }
        });
      }
    }
  }

  onSelectTraining(training: Training) {
    this.selectedTraining = training;

    const formattedDateTime = new Date(training.dateTime).toISOString().slice(0, 16); // yyyy-MM-ddTHH:mm

    this.trainingForm.patchValue({
    ...training,
    exerciseType: training.exerciseType.toString(), // da radi select
    dateTime: formattedDateTime,
    intensity: training.intensity ?? 1,
    fatigue: training.fatigue ?? 1
    });
  }

  getExerciseTypeName(type: number): string {
    switch(type) {
      case 1: return 'Cardio';
      case 2: return 'Strength';
      case 3: return 'Flexibility';
      default: return 'Unknown';
    }
  }
  
  private refreshTrainignList(): void {
    if (this.userId !== undefined) {
      this.trainingService.getTrainingByUserId(this.userId).subscribe(trainings => {
        this.userTrainings = trainings;
        this.selectedTraining = null;
      });
    }
  }

  getWeekOfMonth(date: Date): number {
    const firstDay = new Date(date.getFullYear(), date.getMonth(), 1).getDay(); // dan u nedelji za prvi u mesecu
    const adjustedDate = date.getDate() + firstDay - 1;
    return Math.floor(adjustedDate / 7) + 1;
  }

  getWeeklyStats(trainings: Training[]): WeeklyStat[] {
    const weeksMap = new Map<number, Training[]>();
  
    for (let training of trainings) {
      const date = new Date(training.dateTime);
      const weekNumber = this.getWeekOfMonth(date);
  
      if (!weeksMap.has(weekNumber)) {
        weeksMap.set(weekNumber, []);
      }
      weeksMap.get(weekNumber)?.push(training);
    }
  
    const stats: WeeklyStat[] = [];
    weeksMap.forEach((trainings, week) => {
      const totalDuration = trainings.reduce((sum, t) => sum + t.durationInMinutes, 0);
      const totalTrainings = trainings.length;
      const avgIntensity = +(trainings.reduce((sum, t) => sum + t.intensity, 0) / totalTrainings).toFixed(2);
      const avgFatigue = +(trainings.reduce((sum, t) => sum + t.fatigue, 0) / totalTrainings).toFixed(2);
  
      stats.push({
        week,
        totalDuration,
        totalTrainings,
        avgIntensity,
        avgFatigue
      });
    });
  
    return stats.sort((a, b) => a.week - b.week);
  }
  
  onMonthChange(value: string) {
  this.selectedMonth = value;
  const monthDate = new Date(value + '-01');

  // filtriraj sve treninge izabranog meseca
  const filteredTrainings = this.userTrainings.filter(t => {
    const date = new Date(t.dateTime);
    return date.getMonth() === monthDate.getMonth() && date.getFullYear() === monthDate.getFullYear();
  });

  this.weeklyStats = this.getWeeklyStats(filteredTrainings);
  }

  onMonthInputChange(event: Event) {
    const input = event.target as HTMLInputElement;
    const value = input.value;
    this.onMonthChange(value);
  }

}