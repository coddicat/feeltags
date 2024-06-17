import { defineStore } from 'pinia';
import { useAppStore } from './app';
import { ref } from 'vue';
import {
  getRandomQuestion,
  respondToQuestion,
  type Response,
  type Question
} from '../api/question';
import { getLocation } from '@/exts';

export const useQuestionStore = defineStore('question', () => {
  const homeStore = useAppStore();
  const { toastError } = homeStore;

  const question = ref<Question>();
  const loading = ref(false);

  async function randomQuestion() {
    try {
      loading.value = true;
      question.value = await getRandomQuestion();
    } catch (error) {
      toastError(`${error}`);
    } finally {
      loading.value = false;
    }
  }

  async function _getLocation() {
    try {
      const location = await getLocation();
      return location;
    } catch (error) {
      console.error(error);
      return undefined;
    }
  }

  async function respond(answerOptionId: number) {
    try {
      loading.value = true;
      const location = await _getLocation();
      const response: Response = {
        answerOptionId,
        location
      };
      await respondToQuestion(response);
    } catch (error) {
      toastError(`${error}`);
    } finally {
      loading.value = false;
    }
  }

  return { question, loading, randomQuestion, respond };
});
