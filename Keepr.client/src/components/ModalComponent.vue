<template>
  <div class="modal fade" tabindex="-1" aria-labelledby="keepModalLabel" aria-hidden="true">
    <div class="modal-dialog modal-dialog-centered modal-xl">
      <div class="modal-content">
        <slot>

        </slot>
      </div>
    </div>
  </div>
</template>


<script>
export default {
  setup() {
    return {}
  }
}
</script>


<style lang="scss" scoped>
.modal-content {
  border: none;
}
</style>