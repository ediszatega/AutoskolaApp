import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css']
})
export class SignupComponent implements OnInit {
  type: string="password";
  eyeIcon: string = "fa-eye-slash";
  signupForm!: FormGroup;
  confirmPassword: any;
  constructor (private fb: FormBuilder) { }
  
  ngOnInit(): void {
    this.signupForm = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', Validators.compose([Validators.required])],
      username: ['', Validators.required],
      password: ['', Validators.required],
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

  onSubmit() {
    if(this.signupForm.valid) {
      alert("Form is valid");
      console.log(this.signupForm.value);
    }
    else {
      alert("Form is invalid");
      // this.validateAllFormFields(this.signupForm);
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
