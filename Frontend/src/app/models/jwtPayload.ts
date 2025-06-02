export interface JwtPayload {
    sub: string;
    email: string;
    username: string;
    id: string;
    role?: string;
    exp: number;
  }