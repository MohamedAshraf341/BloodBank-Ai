import { NgModule } from '@angular/core';
import { Routes, RouterModule, PreloadAllModules } from '@angular/router';
import { PagesComponent } from './pages/pages.component';
import { BlankComponent } from './pages/blank/blank.component';
import { SearchComponent } from './pages/search/search.component';
import { NotFoundComponent } from './pages/errors/not-found/not-found.component';
import { HomeComponent } from './site/home/home.component';
import { AboutUsComponent } from './site/about-us/about-us.component';
import { ContactComponent } from './site/contact/contact.component';
import { DonationProcessComponent } from './site/donation-process/donation-process.component';
import { DonationProcessDesComponent } from './site/donation-process-des/donation-process-des.component';
import { WhatHappenDonatebloodComponent } from './site/what-happen-donateblood/what-happen-donateblood.component';
import { FristTimeDonationComponent } from './site/frist-time-donation/frist-time-donation.component';
import { SelectTimeTodonationComponent } from './site/select-time-todonation/select-time-todonation.component';
export const routes: Routes = [
  {
    path: '',
    component: PagesComponent,
    children: [
      { path: '', loadChildren: () => import('./pages/dashboard/dashboard.module').then(m => m.DashboardModule), data: { breadcrumb: 'Dashboard' } },
      { path: 'blank', component: BlankComponent, data: { breadcrumb: 'Blank page' } },
      { path: 'search', component: SearchComponent, data: { breadcrumb: 'Search' } },

    ]
  },
  { path: 'login', loadChildren: () => import('./pages/login/login.module').then(m => m.LoginModule) },
  { path: 'register', loadChildren: () => import('./pages/register/register.module').then(m => m.RegisterModule) },
  {
    path: 'home', component: HomeComponent },
  {path:'about',component:AboutUsComponent},
  {path:'contact',component:ContactComponent},
  {path:'donation-process',component:DonationProcessComponent},
  {path:'donation_process_des',component: DonationProcessDesComponent },
  {path:'what-happen-donateblood',component:WhatHappenDonatebloodComponent},
  {path:"frist-time-donation",component:FristTimeDonationComponent},
  {path:'select-time-todonation',component:SelectTimeTodonationComponent},
  { path: '**', component: NotFoundComponent }
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes, {
      preloadingStrategy: PreloadAllModules, // <- comment this line for activate lazy load
      relativeLinkResolution: 'legacy',
      // useHash: true
    })
  ],
  exports: [
    RouterModule
  ]
})
export class AppRoutingModule { }