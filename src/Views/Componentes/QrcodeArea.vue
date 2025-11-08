<template>
  <div
    class="flex flex-col items-center justify-center bg-white/70 p-6 rounded-2xl shadow-lg border border-[#009FE3]/20"
  >
    <h3 class="text-xl font-bold text-[#004E7D] mb-4">
      QR Code da Área {{ area }}
    </h3>

    <!-- QR Code -->
    <qrcode-vue
      :value="link"
      :size="180"
      class="shadow-md rounded-lg p-2 bg-white"
      level="H"
    />

    <p class="mt-4 text-sm text-gray-500 text-center">
      Escaneie este código para aceder à página da área "{{ area }}"
    </p>

    <button
      @click="baixarQRCode"
      class="mt-6 px-6 py-2 bg-[#009FE3] text-white font-semibold rounded-lg hover:bg-[#007CC0] transition-all"
    >
      Baixar QR Code
    </button>
  </div>
</template>

<script setup>
import QrcodeVue from "qrcode.vue";

const props = defineProps({
  area: String,
  link: String,
});

const baixarQRCode = () => {
  const canvas = document.querySelector("canvas");
  const linkDownload = document.createElement("a");
  linkDownload.href = canvas.toDataURL("image/png");
  linkDownload.download = `qrcode-${props.area}.png`;
  linkDownload.click();
};
</script>
