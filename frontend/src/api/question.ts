import instance from './instance';

export type Question = {
  authorName: string;
  authorPicture: string;
  content: string;
  answerOptions: Record<number, string>;
  createdAt: Date;
};

export type Response = {
  answerOptionId: number;
  location?: Location;
};

export type Location = {
  latitude: number;
  longitude: number;
};

export async function getRandomQuestion(): Promise<Question> {
  const response = await instance.get<Question>('/question');
  return response.data;
}

export async function respondToQuestion(response: Response): Promise<void> {
  await instance.put('/question', response);
}
