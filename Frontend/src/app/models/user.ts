import { Training } from "./training";

export interface User {
    id: number;
    username: string;
    email: string;
    password: string;
    trainings: Training[]; // ili: trainings?: Training[]; ako je opciono
  }