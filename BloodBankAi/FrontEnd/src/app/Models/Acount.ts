export interface loginModel {
    userName: string
    password: string
  }
  export interface responseAuth {
    message: string
    id: string
    isAuthenticated: boolean
    name: string
    username: string
    email: string
    roles: string[]
    token: string
    expiresOn: string
    refreshTokenExpiration: string
  }
  export interface register {
    name: string
    username: string
    email: string
    dateOfBirth: string
    gender: string
    bloodGroup: string
    phoneNumber: string
    area: string
    city: string
    government: string
    password: string
  }