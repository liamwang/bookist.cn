function pad0(n: number) {
  return n < 10 ? '0' + n : n
}

export function formatNumber(num: any, precision = 2, separator = ',') {
  var parts
  // 判断是否为数字
  if (!isNaN(parseFloat(num)) && isFinite(num)) {
    // 把类似 .5, 5. 之类的数据转化成0.5, 5
    num = Number(num)
    // 处理小数点位数
    num = (typeof precision !== 'undefined' ? num.toFixed(precision) : num).toString()
    // 分离数字的小数部分和整数部分
    parts = num.split('.')
    // 整数部分加[separator]分隔, 借用一个著名的正则表达式
    parts[0] = parts[0].toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, '$1' + (separator || ','))

    return parts.join('.')
  }
  return NaN
}

export function formatMonth(val: any, cn?: boolean) {
  if (!val) return ''
  const date = new Date(val)
  return date.getFullYear() + (cn ? '年-' : '-') + pad0(date.getMonth() + 1) + (cn ? '月' : '')
}

export function formatDate(val: any, cn?: boolean) {
  if (!val) return ''
  const date = new Date(val)
  return (
    date.getFullYear() +
    (cn ? '年-' : '-') +
    pad0(date.getMonth() + 1) +
    (cn ? '月-' : '-') +
    pad0(date.getDate()) +
    (cn ? '日' : '')
  )
}

export function formatDateTime(val: any, cn?: boolean) {
  if (!val) return ''
  const date = new Date(val)
  return formatDate(val, cn) + ' ' + pad0(date.getHours()) + ':' + pad0(date.getMinutes())
}

export function formatTime(val: any) {
  if (!val) return ''
  const date = new Date(val)
  return pad0(date.getHours()) + ':' + pad0(date.getMinutes())
}
