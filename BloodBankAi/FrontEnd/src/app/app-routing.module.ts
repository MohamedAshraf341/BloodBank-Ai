import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AboutUsComponent } from './site/about-us/about-us.component';
import { ContactComponent } from './site/contact/contact.component';
import { DonationProcessDesComponent } from './site/donation-process-des/donation-process-des.component';
import { DonationProcessComponent } from './site/donation-process/donation-process.component';
import { FristTimeDonationComponent } from './site/frist-time-donation/frist-time-donation.component';
import { HomeComponent } from './site/home/home.component';
import { SelectTimeTodonationComponent } from './site/select-time-todonation/select-time-todonation.component';
import { WhatHappenDonatebloodComponent } from './site/what-happen-donateblood/what-happen-donateblood.component';
import { FindDonorsComponent } from './site/find-donors/find-donors.component';
import { FindBanksComponent } from './site/find-banks/find-banks.component';
import { AdminPageComponent } from './site/Admin/admin-page/admin-page.component';
import { BankRegisterComponent } from './site/Admin/bank-register/bank-register.component';
import { BankEditComponent } from './site/Admin/bank-edit/bank-edit.component';
import { AdministrationComponent } from './site/administration/administration.component';
import { LoginComponent } from './Auth/login/login.component';
import { SignUpComponent } from './Auth/sign-up/sign-up.component';
import { EditProfileComponent } from './Auth/edit-profile/edit-profile.component';

const routes: Routes = [
  {path: 'home', component: HomeComponent },
  {path:'about',component:AboutUsComponent},
  {path:'contact',component:ContactComponent},
  {path:'donation-process',component:DonationProcessComponent},
  {path:'donation_process_des',component: DonationProcessDesComponent },
  {path:'what-happen-donateblood',component:WhatHappenDonatebloodComponent},
  {path:"frist-time-donation",component:FristTimeDonationComponent},
  {path:'select-time-todonation',component:SelectTimeTodonationComponent},
  {path:'find-donors',component:FindDonorsComponent},
  {path:'find-banks',component:FindBanksComponent},
  {path:'administration',component:AdministrationComponent},
  {path:'Login',component:LoginComponent},
  {path:'SignUp',component:SignUpComponent},
  {path:'EditProfile',component:EditProfileComponent},

  {path:'admin-page'
   ,children:[
    { path: 'bank-register', component: BankRegisterComponent },
          { path: 'bank/:id', component: BankEditComponent },
          { path: '', component: AdminPageComponent },
   ]},  
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  { path: '**', redirectTo: 'home', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
