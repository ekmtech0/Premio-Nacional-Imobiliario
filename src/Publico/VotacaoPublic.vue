<template>
  <section class="border-t border-b border-gray-200 p-4 lg:py-20" id="votacao">
    <div class="max-w-7xl mx-auto pt-12">
      <h1 class="font-montserrat font-bold text-azul text-2xl">Votação do Público</h1>
      <h2 class="font-open text-sm md:text-base text-gray-900 pb-8">
        A sua opinião conta!
      </h2>

      <!-- GRID GERAL - Mobile = 1 coluna, Tablet/Desktop = 3 colunas -->
      <div class="grid grid-cols-1 gap-6 lg:grid-cols-3">

        <!-- FORMULÁRIO -->
        <div class="bg-gray-100 flex flex-col p-6 shadow rounded-xl col-span-1 lg:col-span-2">
          <form class="space-y-4">
            <div class="lg:flex lg:justify-between lg:w-full">
              <div class="">
                <label class="block text-sm font-medium">Nome</label>
                <input type="text" placeholder="Nome completo"
                  class="p-3 border border-gray-300 text-sm rounded-lg lg:w-[380px] w-full ">
              </div>

              <div>
                <label class="block text-sm font-medium">E-mail ou Telemóvel</label>
                <input type="text" placeholder="exemplo@gmail.com"
                  class="p-3 border border-gray-300 text-sm rounded-lg lg:w-[400px] w-full">
              </div>
            </div>
            <div>
              <label class="block text-sm font-medium">Escolha o Nomeado</label>
              <select class="p-3 border border-gray-300 text-sm rounded-lg w-full">
                <option value="" disabled selected>Nomeado</option>
                <option>Victor Makuka</option>
                <option>Linear Comunicações</option>
                <option>EKM Tech</option>
                <option>SG Design</option>
              </select>
            </div>

            <div>
              <label class="block text-sm font-medium">reCAPTCHA</label>
              <div class="w-full h-24 bg-gray-300 rounded-lg flex items-center justify-center text-gray-600">
                Colocar reCAPTCHA aqui
              </div>
            </div>

          </form>

          <!-- Mensagem de sucesso -->
          <span class="text-sm text-verde pt-4">Obrigado por Votar! O seu voto foi registado com sucesso.</span>

          <!-- Botão -->
          <div class="pt-6">
            <button class="rounded-lg p-3 bg-azul text-white w-full font-semibold">Confirmar Voto</button>
          </div>
        </div>

        <!-- ÚLTIMOS VOTOS -->
        <div class="bg-gray-100 flex flex-col p-6 shadow rounded-xl lg:h-1/2">
          <span class="text-azul font-open font-semibold md:text-xl pt-2">Últimos Votos</span>

          <div class="pt-4 flex flex-col space-y-2">
            <span class="text-sm text-gray-700">Séfora Zípora → EKM Tech (21/10/2025, 16:20)</span>
            <span class="text-sm text-gray-700">Mvemba Guilherme → SG Design (21/10/2025, 16:18)</span>
            <span class="text-sm text-gray-700">Steoar Nsingi → SG Design (21/10/2025, 16:17)</span>
            <span class="text-sm text-gray-700">Steoar Nsingi → SG Design (21/10/2025, 16:15)</span>
          </div>
        </div>
        </div>
      

      <!-- FAVORITO DO PÚBLICO -->
      <div class="bg-gray-100 flex flex-col p-6 shadow rounded-xl mt-6 max-w-md mx-auto lg:mx-0">
        <span class="text-azul font-open font-semibold md:text-xl">Favorito do Público (Atual)</span>
        <span class="text-azul font-open font-bold text-lg md:text-xl pt-4">SG Design</span>
        <span class="text-sm text-gray-700">3 votos</span>
      </div>

    </div>
  </section>
</template>






