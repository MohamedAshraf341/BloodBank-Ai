import { Component ,OnInit} from '@angular/core';
import { Router } from '@angular/router';
import {  registerModel } from 'src/app/Models/Acount';
import { HelperService } from 'src/app/Services/helper.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AcountService } from 'src/app/Services/acount.service';
import { NotificationService } from 'src/app/Services/notification.service';

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
  modal: registerModel = {
    name: '',
    username: '',
    email: '',
    dateOfBirth: '',
    gender: '',
    bloodGroup: '',
    phoneNumber: '',
    area: '',
    city: '',
    government: '',
    password: ''
  };
  constructor(private helperService:HelperService
    ,private formBuilder: FormBuilder,private accountService: AcountService,
    private router: Router,
    private notification: NotificationService,){

  }
  ngOnInit(): void {
    this.registerForm = this.formBuilder.group({
      name: ['', Validators.required],
      username: ['', Validators.required],
      password: ['', Validators.required],
      phoneNumber: ['', Validators.required],
      email: ['', Validators.required],
      government: ['', Validators.required],
      city: ['', Validators.required],
      area: ['', Validators.required],
      bloodGroup: ['', Validators.required],
      gender: ['', Validators.required],
      dateOfBirth: ['', Validators.required]
    });
    this.helperService.getAllGov().subscribe((res)=>{
      this.gov=res;
    });
    
    this.helperService.getBloodGroups().subscribe((res)=>{
      this.bloodGroups=res;
    });
    
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

    this.accountService.register(this.registerForm.value).subscribe((res)=>{
      if (res.success) {
        const token = res.data.token;
        const user = res.data;
        this.accountService.setToken(token);
        this.accountService.setUser(user);
        this.router.navigate(['/'])
          .then(() => {            
            window.location.reload();
            this.notification.showSuccess(res.message, "Register");
          });
      }
      else {
        this.notification.showError(res.message, "register")
      }
    });
  }

}

