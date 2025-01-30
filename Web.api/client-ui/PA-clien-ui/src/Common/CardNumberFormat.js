export default function cardNumberFormat(number) {
  const segment = number.match(/.{1,4}/g)

  return segment.join(' - ')
}
