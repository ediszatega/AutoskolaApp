import { group } from '@angular/animations';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  type: string="password";
  eyeIcon: string = "fa-eye-slash";
  loginForm!: FormGroup;
  constructor (private fb: FormBuilder) { }
  
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
    }
    else {
      console.log("Form is not valid");
      this.validateAllFormFields(this.loginForm);
      alert("Invalid form");
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
