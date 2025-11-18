<template>
    <section class="border-t border-b border-gray-200 p-4 lg:py-20" id="nomeado">
        <div class="max-w-7xl mx-auto pt-12">
            <h1 class="font-montserrat font-bold text-azul text-2xl text-center">Login</h1>
            <h2 class="font-open md:text-base text-gray-600 pb-8 text-center text-xs">
               Seja bem-vindo de volta! Por favor, insira suas credenciais para acessar o painel administrativo.
            </h2>

              <!-- FORMULÁRIO DE LOGIN -->
<div class="bg-gray-100 flex flex-col p-6 shadow rounded-xl col-span-1 lg:col-span-2 mt-8 mx-auto max-w-md">
          <form class="space-y-4">
            <div class="">
              <div class="">
                <label class="block text-sm font-medium">E-mail</label>
                <input v-model="Email" type="text" placeholder="exemplo@gmail.com"
                  class="p-3 border border-gray-300 text-sm rounded-lg  w-full ">
              </div>

              <div>
                <label class="block text-sm font-medium">Sua senha</label>
                <input v-model="senha" type="password" placeholder="********"
                  class="p-3 border border-gray-300 text-sm rounded-lg  w-full">
              </div>
            </div>
          </form>

          <div class="flex justify-between mt-2">
            <label for="lembrar">Lembrar de mim</label>
            <input type="checkbox" id="lembrar" v-model="lembrarMe" class="w-4">
          </div>

          <!-- Mensagem de sucesso -->
          <span class="text-sm text-verde pt-4">{{ msg }}</span>

          <!-- Botão -->
          <div  class="pt-6" >
            <button class="rounded-lg p-3 bg-azul text-white w-full font-semibold"  @click="logar()">Entrar</button>
          </div>
        </div>

           </div>
    </section>
</template>

<script setup>
import { ref } from 'vue';
import { useRouter } from 'vue-router'
import { http } from '@/Request/api';
const Email = ref('');
const senha = ref('');
const lembrarMe = ref(false)
const router = useRouter()
const msg = ref('')


async function logar() {
  try {
    const response = await http.post('adm/login', {
      email: Email.value,
      senha: senha.value,
      rememberMe: lembrarMe.value
    },{ withCredentials: true });
    const data = response.data;
    console.log(data)
    if (data) {
      router.push('/adm/DashboardPNI');
    } else {
      alert('Credenciais inválidas. Tente novamente.');
    }
  } catch (error) {
    console.error('Erro ao realizar login:', error);
    alert('Ocorreu um erro. Tente novamente mais tarde.');
  }
}

</script>


<style>
/* Esconder scrollbar apenas no mobile */
.hide-scrollbar::-webkit-scrollbar {
    display: none;
}
.hide-scrollbar {
    -ms-overflow-style: none;
    scrollbar-width: none;
}
</style>
