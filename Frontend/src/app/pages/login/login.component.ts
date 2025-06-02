import { Component } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { NavbarComponent } from '../../components/navbar/navbar.component';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthServiceService } from '../../services/auth-service.service';
import { JwtHelperService } from '../../services/jwtHelper.service';
import { JwtPayload } from '../../models/jwtPayload';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [RouterModule, NavbarComponent, ReactiveFormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {

  loginForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private authService: AuthServiceService,
    private router: Router,
    private jwtHelper: JwtHelperService
  ) {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]], //mora ici @ u mail!!!
      password: ['', Validators.required]
    });
  }

  onSubmit(): void {
    if (this.loginForm.valid) {
      this.authService.login(this.loginForm.value).subscribe({
        next: (response: { token: string }) => {
          localStorage.setItem('token', response.token);
  
          const decoded: JwtPayload | null = this.jwtHelper.decodeToken(response.token);
          console.log('Dekodirani token:', decoded);
  
          if (decoded) {
            localStorage.setItem('userId', decoded.id);
            localStorage.setItem('email', decoded.email);
            localStorage.setItem('username', decoded.sub);
            if (decoded.role) {
              localStorage.setItem('role', decoded.role);
            }
          }
  
          this.router.navigate(['/dashbord']);
        },
        error: err => {
          console.error('Greška prilikom logovanja:', err);
        }
      });
    }
  }

}
