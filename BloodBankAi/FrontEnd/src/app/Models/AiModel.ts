export interface requestAi {
    recency__months_: number,
    frequency__times_: number,
    monetary__c_c__blood_: number,
    time__months_: number
  }
  export interface responsAi {
    recency__months_: number;
    frequency__times_: number;
    monetary__c_c__blood_: number;
    time__months_: number;
    whether_he_she_donated_blood_in_March_2007: number;
    features?: number[];
    predictedLabel: number;
    score?: number[];
  }