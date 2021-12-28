import { Component, OnInit } from '@angular/core';
import { DataService, AuthenticationResponse, AuthenticationRequest } from '../data.service';
import {Validators, FormGroup, FormBuilder} from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  form: FormGroup;
  isLoggedIn = false;
  isLoginFailed = false;
  errorMessage = '';
  tokenKey = "TOKEN_KEY";
  userKey = "USER_KEY";

  constructor(
    private dataService : DataService,
    private fb: FormBuilder,
    private route: Router) {
    this.form = this.fb.group({
      email: [null, Validators.required, Validators.email],
      password: [null, Validators.required]
    });
  }

  ngOnInit(): void {
    if(window.sessionStorage.getItem(this.tokenKey))
    {
      this.isLoggedIn = true;
    }
  }

  onSubmit(): void {

    console.log(this.form.getRawValue());


    let request = new AuthenticationRequest();
    request.email = this.form.controls['email'].value;
    request.password = this.form.controls['password'].value;

    this.dataService.authenticate(request).subscribe(
      (data : AuthenticationResponse) => {
        console.log(JSON.stringify(data));
        window.sessionStorage.removeItem(this.tokenKey);
        window.sessionStorage.removeItem(this.userKey);
        window.sessionStorage.setItem(this.tokenKey, data.token!);
        window.sessionStorage.setItem(this.userKey, data.userName!);
        this.isLoginFailed = false;
        this.isLoggedIn = true;
        this.reloadPage();
      },
      err => {
        this.errorMessage = err.error.message;
        this.isLoginFailed = true;
      }
    );
  }

  reloadPage(): void {
    this.route.navigate(['/movies']);
  }

}
