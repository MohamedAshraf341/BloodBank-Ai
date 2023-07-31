import { Component, OnInit } from '@angular/core';
import { requestAi,responsAi } from 'src/app/Models/AiModel';
import { HelperService } from 'src/app/Services/helper.service';
import { NotificationService } from 'src/app/Services/notification.service';

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
  p='';
respns:responsAi;
  display: boolean=false;
  constructor(private helperService:HelperService,    private notification: NotificationService,
    ) { }
  continue(){
    this.display=true; 
    // debugger
      this.helperService.getAiPrediction(this.model).subscribe((res)=>{
        // if(res.success )
        // {
        //   this.display=true;       
        // }
        if(res.data.predictedLabel>0)
        {
          this.notification.showSuccess('u  can donate your blood ','predictdonation')
          // this.p='u  can donate your blood ';
        }
        else{
          this.notification.showError('sorry u cant can donate your blood beaucue your health ','predictdonation')

          // this.p='sorry u cant can donate your blood beaucue your health';
        }
      });
    
    
   }
  ngOnInit(): void {
  }
}
