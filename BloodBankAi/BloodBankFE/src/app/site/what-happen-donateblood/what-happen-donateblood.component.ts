import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-what-happen-donateblood',
  templateUrl: './what-happen-donateblood.component.html',
  styleUrls: ['./what-happen-donateblood.component.scss']
})
export class WhatHappenDonatebloodComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }
  ngAfterViewInit(){
    document.getElementById('preloader').classList.add('hide');                 
}
}
