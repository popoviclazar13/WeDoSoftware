import { Training } from "./training";

export interface User {
    id: number;
    username: string;
    email: string;
    password: string;
    trainings: Training[]; 
  }