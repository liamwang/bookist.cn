String.prototype.trimStart = function (charlist) {
  if (charlist === undefined) charlist = ' '
  return this.replace(new RegExp('^[' + charlist + ']+'), '')
}

String.prototype.trimEnd = function (charlist) {
  if (charlist === undefined) charlist = ' '
  return this.replace(new RegExp('[' + charlist + ']+$'), '')
}

String.prototype.trim = function (charlist) {
  return this.trimStart(charlist).trimEnd(charlist)
}
