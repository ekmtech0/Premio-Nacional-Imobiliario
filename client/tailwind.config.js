/** @type {import('tailwindcss').Config} */
export default {
  content: [
        "./index.html",
    "./src/**/*.{vue,js,ts,jsx,tsx}",
  ],
  theme: {
    extend: {
      colors: {
        azul: '#003366',
        cinza: '#666666',
        verde:'#009966',
      },
      fontFamily: {
            montserrat: ['Montserrat', 'sans-serif'],
        open: ['Open Sans', 'sans-serif']
      },
    },
  },
  plugins: [],
}

