import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NgToastService } from 'ng-angular-popup';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css']
})
export class SignupComponent implements OnInit {
  type: string="password";
  typeConfirm: string="password";
  eyeIcon: string = "fa-eye-slash";
  eyeIconConfirm: string = "fa-eye-slash";
  signupForm!: FormGroup;
  confirmPassword: any;

  constructor (
    private fb: FormBuilder, 
    private service: AuthService, 
    private router: Router,
    private toast: NgToastService
    ) { }
  
  ngOnInit(): void {
    this.signupForm = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', Validators.compose([Validators.required, Validators.email])],
      username: ['', Validators.required],
      password: ['', Validators.compose([Validators.required,
         Validators.pattern(/(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,}/)])],
      confirmPassword: ['']
    },
    {
      validator: this.ConfirmedValidator('password', 'confirmPassword'),
    })
  } 

  hideShowPass() {
    if(this.type==="password")  {
      this.type = "text";
      this.eyeIcon = "fa-eye"
    }
    else {
      this.type="password";
      this.eyeIcon="fa-eye-slash"
    }
  }

  hideShowConfirmPass() {
    if(this.typeConfirm==="password")  {
      this.typeConfirm = "text";
      this.eyeIconConfirm = "fa-eye"
    }
    else {
      this.typeConfirm="password";
      this.eyeIconConfirm="fa-eye-slash"
    }
  }
  onSubmit() {
    if(this.signupForm.valid) {
      var signupObject = this.mapToSignupObject(this.signupForm.value);
      this.service.register(signupObject).subscribe({
        next:(res)=>{
          if(res.statusCode === 200){
            this.toast.success({summary:"SUCCESS", detail:res.message, duration: 5000});
            this.signupForm.reset();
            this.router.navigate(['login']);
          }
        },
        error:(err)=>{
          this.toast.success({summary:"ERROR", detail:err?.error.Message, duration: 5000});
          console.log(err);
        }
      });
    }
  }

  mapToSignupObject(formValue: any) {
    return {
      firstName:formValue.firstName,
      lastName:formValue.lastName,
      email: formValue.email,
      username: formValue.username,
      password: formValue.password
    };
  }

  ConfirmedValidator(controlName: string, matchingControlName: string) {
    return (formGroup: FormGroup) => {
      const control = formGroup.controls[controlName];
      const matchingControl = formGroup.controls[matchingControlName] 
      this.confirmPassword = matchingControl;
      if (
        matchingControl.errors &&
        !matchingControl.errors?.["confirmedValidator"]
      ) {
        return;
      }
      if (control.value !== matchingControl.value) {
        matchingControl.setErrors({ confirmedValidator: true });
      } else {
        matchingControl.setErrors(null);
      }
    };
  }
}
