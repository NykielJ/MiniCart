<template>
  <div></div>
</template>

<script>
import Swal from "sweetalert2";

export default {
  props: {
    message: {
      type: String,
      default: "Are you sure?",
    },
  },
  methods: {
    showDialog() {
      Swal.fire({
        title: "Are you sure?",
        text: this.message,
        showCancelButton: true,
        confirmButtonColor: "#d33",
        cancelButtonColor: "#3085d6",
        confirmButtonText: "Yes, remove it!",
      }).then((result) => {
        if (result.isConfirmed) {
          this.$emit("confirm");
        } else {
          this.$emit("cancel");
        }
      });
    },
  },
};
</script>
