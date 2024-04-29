export type UserModel = {
  id: number,
  name: string,
  email: string,
  profilePicture: string
}

export type SetUserModel = {
  name: string,
  password: string,
  email?: string
}

export type UpdateUserModel = {
  name?: string,
  password?: string,
  email?: string,
  profilePicture: string
}

export type AuthModel = {
  user: UserModel,
  token: string
}
