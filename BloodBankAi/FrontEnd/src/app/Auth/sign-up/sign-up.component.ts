import { Component ,OnInit} from '@angular/core';
import { Router } from '@angular/router';
import { registerModel } from 'src/app/Models/Acount';
import { HelperService } from 'src/app/Services/helper.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.scss']
})
export class SignUpComponent implements OnInit{

  registerForm: FormGroup;
  submitted = false;
  gov:any[]=[];
  cit:any[]=[];
  bloodGroups:any[]=[];
  selectedDate:any;

  constructor(private helperService:HelperService
    ,private formBuilder: FormBuilder){

  }
  ngOnInit(): void {
    this.registerForm = this.formBuilder.group({
      name: ['', Validators.required],
      username: ['', Validators.required],
      password: ['', Validators.required],
      confirmPassword: ['', Validators.required],
      phoneNumber: ['', Validators.required],
      email: ['', Validators.required],
      government: ['', Validators.required],
      city: ['', Validators.required],
      area: ['', Validators.required],
      bloodGroup: ['', Validators.required],
      gender: ['', Validators.required],
      dateOfBirth: ['', Validators.required]
    }, {
      validator: this.matchPassword
    });
    this.helperService.getAllGov().subscribe((res)=>{
      this.gov=res;
    });
    
    this.helperService.getBloodGroups().subscribe((res)=>{
      this.bloodGroups=res;
    });
    
  }
  matchPassword(group: FormGroup) {
    const password = group.controls['password'].value;
    const confirmPassword = group.controls['confirmPassword'].value;
    return password === confirmPassword ? null : { passwordMismatch: true };
  }
  onSelectChange(value: any) 
  { 
    this.helperService.getAllCit(value).subscribe((res)=>{
      this.cit=res;
    });
   }


   onSubmit() {
    this.submitted = true;
    // Stop here if form is invalid
    if(this.registerForm.invalid) {
      return;
    }
    // Display form values on console
    console.log(this.registerForm.value);
  }

}

