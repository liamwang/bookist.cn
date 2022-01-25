import http from '../utils/http'

export const getTags = (params?: object) => http.get(`tags`, params)

// export const getTagById = (id: number) => http.get(`tags/${id}`)

export const saveTag = (data: object) => http.post(`tags`, data)

export const deleteTag = (id: number) => http.delete(`tags/${id}`)
