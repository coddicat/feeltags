import type { User } from 'firebase/auth';
import instance from './instance';

export type Account = {
  name: string;
  email: string;
  picture: string;
};

export async function verifyGoogle(user: User): Promise<Account> {
  const idToken = await user.getIdToken();

  const response = await instance.post<Account>('/auth/google', {
    idToken: idToken
  });
  return response.data;
}

export async function check(): Promise<Account> {
  const response = await instance.get<Account>('/auth');
  return response.data;
}

export async function logout() {
  const response = await instance.delete('/auth');
  return response.data;
}
