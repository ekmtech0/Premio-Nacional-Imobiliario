<template>
  <section class="pt-20 px-4 md:px-8 lg:px-16  min-h-screen flex justify-center items-center">
    <div
      class="bg-gray-200 rounded-2xl p-6 md:p-10 w-full max-w-sm sm:max-w-md lg:max-w-lg flex flex-col items-center text-gray-400 shadow-lg"
    >
      <!-- Logo -->
      <div class="text-center flex flex-col items-center pt-2">
        <img src="" alt="Logo Prémio Naciona Imobiliario" class="h-8 sm:h-10" />
        <h2 class="text-sm sm:text-base text-gray-700 mt-2">Verificação de Email 📩</h2>
      </div>

      <!-- Texto de instrução -->
      <p class="text-center text-sm text-gray-700 mt-4">
        Enviámos um código de verificação para o teu email.<br />
        Insere o código abaixo para conluir a verificação.
      </p>

      <!-- Formulário -->
      <form action="" class="w-full mt-6 space-y-4">
        <div>
          <label for="codigo" class="block text-sm font-medium text-black  mb-1">Código de Verificação</label>
          <input
            type="text"
            id="codigo"
            placeholder="Ex: 123456"
            maxlength="6"
            class="w-full tracking-widest text-center text-lg px-3 py-2 bg-preto2 border border-gray-600 rounded-xl text-gray-200 focus:outline-none focus:ring-2 focus:ring-azul focus:border-amarelo transition"
          />
        </div>

        <button
          @click="VoltarForm3()"
          type="submit"
          class="w-full bg-azul text-preto font-semibold py-2 px-4 rounded-md hover:bg-yellow-600 transition-colors duration-300"
        >
          Confirmar Código
        </button>
      </form>

      <!-- Reenviar código -->
      <p class="text-center text-sm text-gray-700 mt-4">
        Não recebeste o código?
        <button @click="reenviarCodigo" class="text-azul hover:underline">Reenviar</button>
      </p>

    </div>
  </section>
</template>

<script setup>
const emit = defineEmits([ 'AbrirForm3']);


function VoltarForm3() {
  emit('AbrirForm3')
}
</script>
