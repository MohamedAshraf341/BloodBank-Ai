import { Component } from '@angular/core';
import { loginModel } from 'src/app/Models/Acount';
import { AcountService } from 'src/app/Services/acount.service';
import { Router } from '@angular/router';
import { NotificationService } from 'src/app/Services/notification.service';
import { responseAuth } from 'src/app/Models/Acount';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {

  invalidLogin: boolean;
  modal: loginModel = {
    userName: '',
    password: ''
  };
  constructor(
    private accountService: AcountService,
    private router: Router,
    private notification: NotificationService,
  ) { }

  onSubmit() {
    this.accountService.login(this.modal).subscribe((res) => {
      if (res.success) {
        const token = res.data.token;
        const user = res.data;
        this.accountService.setToken(token);
        this.accountService.setUser(user);
        this.router.navigate(['/'])
          .then(() => {            
            window.location.reload();
            this.notification.showSuccess(res.message, "Login");
          });
      }
      else {
        this.notification.showError(res.message, "Login")
      }
    });
  }
}
