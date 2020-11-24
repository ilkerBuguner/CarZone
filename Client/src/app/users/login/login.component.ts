import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth/auth.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  loginForm : FormGroup;
  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router,
    private toastrService: ToastrService) { 
    this.loginForm = this.fb.group( {
      'username': ['', [Validators.required]],
      'password': ['', [Validators.required]]
    })
  }

  ngOnInit(): void {
  }

  login() {
    if (this.loginForm.invalid) {
      this.toastrService.error('Please populate username and password field!')
      return;
    }
    this.toastrService.info('Loading...')
    
    this.authService.login(this.loginForm.value).subscribe(data => {
      const userId = data.user.id;
      const username = data.user.userName;
      const token = data.token;
      const isAdmin = data.isAdmin;
      this.authService.setUserInfo(userId, username, token, isAdmin);
      this.toastrService.clear();
      this.toastrService.success("Logged in successfully!");
      this.router.navigate(["advertisements"]);
    })
  }

  get username() {
    return this.loginForm.get('username');
  }

  get password() {
    return this.loginForm.get('password');
  }

}
