export interface IUser {
    id: string;
    username: string;
    profilePictureUrl: string;
    fullName?: string;
    phoneNumber?: string;
    email?: string;
    createdOn?: string;
    location?: string;
    gender?: string;
}