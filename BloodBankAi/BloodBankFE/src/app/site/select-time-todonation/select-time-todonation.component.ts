import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-select-time-todonation',
  templateUrl: './select-time-todonation.component.html',
  styleUrls: ['./select-time-todonation.component.scss']
})
export class SelectTimeTodonationComponent implements OnInit {
  answer:string='';
  answer1:string='';
  answer2:string='';
  answer3:string='';
  display: boolean;
  constructor() { }
  continue(){
      
    if(this.answer=="no"||this.answer1=="no"||this.answer2=="no"||this.answer3=="no"){
      alert("sorry u cant can donate your blood beaucue your health")
      this.display=true;
 
    }
    else{
     this.display=false;
    }
    
   }
  ngOnInit(): void {
  }
  ngAfterViewInit(){
    document.getElementById('preloader').classList.add('hide');                 
}
}
