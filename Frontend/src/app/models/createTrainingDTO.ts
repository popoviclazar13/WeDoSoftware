import { ExerciseType } from "./exercise-type";

export interface CreateTrainingDto {
    userId: number;
    dateTime: string;
    exerciseType: ExerciseType;
    durationInMinutes: number;
    calories: number;
    intensity: number;
    fatigue: number;
    notes?: string;
}