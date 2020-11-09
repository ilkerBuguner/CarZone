import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  registerForm: FormGroup
  constructor(private fb: FormBuilder,
     private authService: AuthService,
      private router: Router) {
    this.registerForm = this.fb.group({
      'username': ['', [Validators.required, Validators.minLength(3), Validators.maxLength(30)]],
      'fullName': ['', [Validators.required, Validators.minLength(3), Validators.maxLength(30)]],
      'email': ['', [Validators.required]],
      'password': ['', [Validators.required, Validators.minLength(6)]]
    })
   }

  ngOnInit(): void {
  }

  register() {
    if(this.registerForm.invalid) {
      return;
    }

    this.authService.register(this.registerForm.value).subscribe(data => {
      this.authService.saveToken(data['token']);
    })
  }

  navigateTo(route : string) {
    this.router.navigate([route]);
  }

  get username() {
    return this.registerForm.get('username');
  }

  get fullName() {
    return this.registerForm.get('fullName');
  }

  get email() {
    return this.registerForm.get('email');
  }

  get password() {
    return this.registerForm.get('password');
  }

}