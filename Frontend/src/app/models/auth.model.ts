import {UserModel} from "./user.model";

export type AuthResponseModel = {
  user: UserModel,
  token: string
}

export type AuthRequestModel = {
  name: string,
  email?: string,
  password: string
}
