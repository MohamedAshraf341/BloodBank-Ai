interface address {
    id: number;
    area: string;
    city: string;
    government: string;
    country: string;
  }
  
  export interface banks {
    id: number;
    name: string;
    picture: string;
    address: address;
  }
  export interface bank {
    id: number
    name: string
    phoneNumber: string
    email: string
    website: string
    lastUpdated: string
    picture: string
    address: address
    moderators: Moderator[]
  }  
   interface Moderator {
    id: number
    userId: string
    user: User
    bankId: number
    type: string
  }
  
  interface User {
    name: string
    userName: string
    picture: string
  }
  export interface addModerator{
    bankId:number
    userName:string
    roles:number
  }
  export interface addbank {
    name: string
    phoneNumber: string
    email: string
    website: string
    area: string
    city: string
    government: string
    country: string
    userName: string
  }