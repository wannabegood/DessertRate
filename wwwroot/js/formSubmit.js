window.submitForm = (form) => {
  if (form && form.submit) {
    form.submit();
  } else {
    console.warn("submitForm: form reference is null or has no submit method");
  }
};
