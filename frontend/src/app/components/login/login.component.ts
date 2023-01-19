import { group } from '@angular/animations';
import { Component, NO_ERRORS_SCHEMA, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NgToastService } from 'ng-angular-popup';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  type: string="password";
  eyeIcon: string = "fa-eye-slash";
  loginForm!: FormGroup;
  constructor (
    private fb: FormBuilder, 
    private service: AuthService, 
    private router: Router,
    private toast: NgToastService
    ) { }
  
  ngOnInit(): void {
    this.loginForm = this.fb.group({
      username:['', Validators.required],
      password:['', Validators.required]
    })
  } 

  hideShowPass() {
    if(this.type==="password")  {
      this.type = "text";
      this.eyeIcon = "fa-eye";
    }
    else {
      this.type="password";
      this.eyeIcon="fa-eye-slash";
    }
  }

  onSubmit() {
    if(this.loginForm.valid) {
      console.log(this.loginForm.value);
      this.service.login(this.loginForm.value).subscribe({
        next:(res)=>{
          console.log(res);
          if(res.statusCode === 200){
            this.toast.success({detail:"SUCCESS", summary:res.message, duration: 5000})
            this.loginForm.reset();
            this.router.navigate(['dashboard']);
          }
          else
            this.toast.error({detail:"Error", summary:res.Message, duration: 5000})

        },
        error:(err)=>{
          console.log(err);
          this.toast.error({detail:"Error", summary:err?.error.Message, duration: 5000})
        }
      });
    }
    else {
      this.validateAllFormFields(this.loginForm);
    }
  }

  private validateAllFormFields(formGroup:FormGroup) {
    Object.keys(formGroup.controls).forEach(field=>{
      const control = formGroup.get(field);
      if(control instanceof FormControl){
        control?.markAsDirty({onlySelf:true});
      }
      else if(control instanceof FormGroup){
        this.validateAllFormFields(control);
      }
    });
  }
}
