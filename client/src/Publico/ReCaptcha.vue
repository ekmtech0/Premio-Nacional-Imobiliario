<template>
  <div ref="recaptchaContainer"></div>
</template>

<script setup>
import { ref, onMounted } from 'vue'

const props = defineProps({
  siteKey: { type: String, required: true }
})

const emit = defineEmits(['verified'])
const recaptchaContainer = ref(null)

onMounted(() => {
  // Se já existe o grecaptcha no window
  if (window.grecaptcha && window.grecaptcha.render) {
    renderRecaptcha()
  } else {
    // Cria o script e espera o carregamento completo
    const script = document.createElement('script')
    script.src = 'https://www.google.com/recaptcha/api.js?onload=onRecaptchaLoad&render=explicit'
    script.async = true
    script.defer = true
    document.head.appendChild(script)

    // Define a função global que o Google chama quando o script carrega
    window.onRecaptchaLoad = () => {
      renderRecaptcha()
    }
  }
})

function renderRecaptcha() {
  if (!window.grecaptcha || !window.grecaptcha.render) {
    console.warn('grecaptcha ainda não disponível, tentando novamente...')
    setTimeout(renderRecaptcha, 500)
    return
  }

  window.grecaptcha.render(recaptchaContainer.value, {
    sitekey: props.siteKey,
    callback: (token) => emit('verified', token)
  })
}
</script>
