import { Injectable } from '@angular/core';
import { jwtDecode } from 'jwt-decode';
import { JwtPayload } from '../models/jwtPayload';

@Injectable({
  providedIn: 'root'
})
export class JwtHelperService {

  decodeToken(token: string): JwtPayload | null {
    try {
      return jwtDecode<JwtPayload>(token);
    } catch (error) {
      console.error('Neuspešno dekodiranje tokena', error);
      return null;
    }
  }

  getDecodedToken(): JwtPayload | null {
    const token = localStorage.getItem('token');
    return token ? this.decodeToken(token) : null;
  }

  isTokenExpired(): boolean {
    const decoded = this.getDecodedToken();
    if (!decoded) return true;
    const now = Date.now() / 1000;
    return decoded.exp < now;
  }
}