interface bloodGroup {
    id: number;
    group: string;
    value: number;
  }
  export interface bankWithBloodGroups {
    id: number;
    name: string;
    bloodGroups: bloodGroup[];
  }
  interface address {
    id: number;
    area: string;
    city: string;
    government: string;
    country: string;
  }
  export interface bankByIdWithAddress {
    id: number;
    name: string;
    phoneNumber: string;
    email: string;
    website: string;
    lastUpdated: string;
    picture: string;
    address: address;
  }