import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { NavbarComponent } from '../../components/navbar/navbar.component';
import { FormGroup, FormBuilder, Validators, ReactiveFormsModule } from '@angular/forms';
import { UserService } from '../../services/user.service';
import { Router } from '@angular/router';

@Component({
  standalone: true,
  selector: 'app-register',
  imports: [ReactiveFormsModule, RouterModule, NavbarComponent],
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
  registerForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private userService: UserService,
    private router: Router
  ) {
    console.log('RegisterComponent učitan');
    this.registerForm = this.fb.group({
      username: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]], //mora ici @ u mail!!!
      password: ['', Validators.required]
    });
  }
  consoleTest() {
    console.log('Klik dugmeta radi.');
  }

  onSubmit(): void {
    alert('klik');
    console.log('Submit kliknut');
    if (this.registerForm.valid) {
      const formValue = this.registerForm.value;
      this.userService.createUser(formValue).subscribe({
        next: () => {
          console.log('Uspešna registracija');
          this.router.navigate(['/login']); 
        },
        error: err => {
          console.error('Greška prilikom registracije:', err);
        }
      });
    }
  }
}
