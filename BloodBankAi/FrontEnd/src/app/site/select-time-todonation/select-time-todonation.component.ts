import { Component, OnInit } from '@angular/core';
import { requestAi,responsAi } from 'src/app/Models/AiModel';
import { HelperService } from 'src/app/Services/helper.service';

@Component({
  selector: 'app-select-time-todonation',
  templateUrl: './select-time-todonation.component.html',
  styleUrls: ['./select-time-todonation.component.scss']
})
export class SelectTimeTodonationComponent implements OnInit {
  model:requestAi={
    recency__months_: 0,
    frequency__times_: 0,
    monetary__c_c__blood_: 0,
    time__months_: 0,
  }
respns:responsAi;
  display: boolean;
  constructor(private helperService:HelperService) { }
  continue(){
      this.helperService.getAiPrediction(this.model).subscribe((res)=>{
        if(res.success )
        {
          this.display=true;       
        }
        else{
          this.display=false;
          alert("sorry u cant can donate your blood beaucue your health")
        }
      });
    
    
   }
  ngOnInit(): void {
  }
}
