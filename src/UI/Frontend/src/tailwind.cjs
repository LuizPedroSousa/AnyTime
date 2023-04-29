/** @type {import('tailwindcss').Config} */
module.exports = {
    content: ['./src/**/*.{js,ts,jsx,tsx}'],
    theme: {
        extend: {
            colors: {
                black: '#0d0d0d',
                box: '#21222D',
                primary: '#20AEF3',
                secondary: '#64CFF6',
                bg: '#171821',
            },

            fontFamily: {
                mono: ['Inter'],
            },
        },
    },
    plugins: [],
};
