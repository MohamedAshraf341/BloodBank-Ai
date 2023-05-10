import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AdminService } from 'src/app/Services/admin.service';
import { NotificationService } from 'src/app/Services/notification.service';

@Component({
  selector: 'app-bank-register',
  templateUrl: './bank-register.component.html',
  styleUrls: ['./bank-register.component.scss']
})
export class BankRegisterComponent implements OnInit {
  bankRegisterForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private adminService: AdminService,
    private toastr: NotificationService,
    private router: Router
  ) {} 
  ngOnInit(): void {
    this.initilizeForm();
  }

  initilizeForm() {
    this.bankRegisterForm = this.fb.group({
      name: ['', Validators.required],
      website: [''],
      phoneNumber: ['', Validators.required],
      email: ['', [Validators.email, Validators.required]],
      area: ['', Validators.required],
      city: ['', Validators.required],
      government: ['', Validators.required],
      userName: ['', Validators.required],
    });
  }
  registerBank() {
    console.log(this.bankRegisterForm.value);
    // this.adminService
    //   .addBank(this.bankRegisterForm.value)
    //   .subscribe((res) => {
    //     if(res.success)
    //     {
    //       this.toastr.showSuccess(res.message,"Add Bank")
    //       this.router.navigateByUrl('/admin-page' );
    //     }
    //     else
    //     {
    //       this.toastr.showError(res.message,"Add Bank")
    //     }
    //   });
  }
}
