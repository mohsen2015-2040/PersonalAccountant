const required = (value) => !!value || '!این مورد نمی تواند خالی باشد'
const cardNumberLength = (value) => /^\d{16}$/.test(value) || '!شماره کارت باید شامل 16 رقم باشد'
const passwordLength = (value) => value.length > 3 || '!طول رمز باید حداقل 4 کاراکتر باشد'
const confirmPassword = (value, originPass) =>
  value === originPass || '!پسورد ها با هم منطبق نیستند'
const txtFieldMaxLength = (value) => value == null || value.length <= 50 || '!محدودیت ورودی'

export { required, cardNumberLength, confirmPassword, passwordLength, txtFieldMaxLength }
