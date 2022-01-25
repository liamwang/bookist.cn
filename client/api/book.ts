import http from '../utils/http'

export const getBooks = (params: object) => http.get(`books`, params)

export const getBookById = (id: number) => http.get(`books/${id}`)

export const saveBook = (data: object) => http.post(`books`, data)

export const deleteBook = (id: number) => http.delete(`books/${id}`)

export const getQiniuToken = async () =>
  http.get<any>('upload/qiniuToken').then((data) => data.value)

export const uploadBookCover = async (file: File) => {
  let token = await getQiniuToken()
  let formData = new FormData()
  formData.append('file', file)
  formData.append('token', token)
  return fetch('//upload.qiniu.com/', { method: 'POST', body: formData })
    .then((res) => res.json())
    .then((data) => {
      console.log(data)
    })
    .catch((error) => {
      console.error(error)
    })
}
