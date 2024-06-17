<script setup lang="ts">
import { useQuestionStore } from '@/store/question';
import { storeToRefs } from 'pinia';
import { onMounted } from 'vue';
const questionStore = useQuestionStore();
const { randomQuestion, respond } = questionStore;
const { question, loading } = storeToRefs(questionStore);

const severity = ['success', 'info', 'warning', 'help', 'danger', 'contrast'];

async function respondToQuestion(answerOption: number) {
  await respond(answerOption);
  await randomQuestion();
}

onMounted(async () => {
  await randomQuestion();
});
</script>

<template>
  <div class="home flex flex-column">
    <div class="flex-grow-1"></div>
    <div
      class="flex-grow-0 flex flex-column justify-content-center align-items-center"
    >
      <Fieldset class="m-2">
        <template #legend>
          <div v-if="loading" class="px-2">
            <Skeleton
              shape="rectangle"
              border-radius="16px"
              width="10rem"
              height="2rem"
            />
          </div>
          <div v-else class="flex align-items-center pl-2">
            <Avatar image="logo.png" shape="circle" />
            <span>Admin</span>
          </div>
        </template>
        <div v-if="loading" class="mx-6 flex flex-column gap-2">
          <h3>
            <Skeleton shape="rectangle" width="17rem" height="2.5rem" />
          </h3>
          <div class="flex flex-row gap-3 justify-content-center flex-wrap">
            <Skeleton shape="rectangle" width="5rem" height="2rem" />
            <Skeleton shape="rectangle" width="5rem" height="2rem" />
            <Skeleton shape="rectangle" width="5rem" height="2rem" />
          </div>
        </div>
        <div v-else class="mx-6 flex flex-column gap-2">
          <h3>{{ question?.content }}</h3>
          <div class="flex flex-row gap-3 justify-content-center flex-wrap">
            <Button
              v-for="(text, id, $i) in question?.answerOptions"
              :key="id"
              :label="text"
              :severity="severity[$i % 6]"
              @click="respondToQuestion(id)"
            />
          </div>
        </div>
        <div class="skip-btn flex flex-row justify-content-end">
          <Skeleton
            v-if="loading"
            shape="rectangle"
            width="4rem"
            height="36px"
          />

          <Button
            v-else
            label="Next"
            severity="secondary"
            @click="randomQuestion"
          />
        </div>
      </Fieldset>
    </div>
    <div class="flex-grow-1"></div>
  </div>
</template>

<style lang="scss">
.skip-btn {
  margin-bottom: -4.5rem;
  margin-top: 2.5rem;
}
.home {
  min-height: 100vh;
  margin-top: -4rem; //header
  padding-top: 4rem;
  padding-bottom: 4rem;
  min-width: 20rem;
}
</style>
