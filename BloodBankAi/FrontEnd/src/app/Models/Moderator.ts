export interface BloodGroupUpdateDto {
    bankId: number;
    groups?: BloodGroupDto[];
  }
  
  export interface BloodGroupDto {
    id: number;
    group?: string;
    value: number;
  }