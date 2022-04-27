import axios from 'axios'

const apiClient = axios.create({
    baseURL: 'https://localhost:7006/api/recipe',
    withCredentials: false,
    headers: {
        Accept: 'application/json',
        'Content-Type': 'application/json'
    }
})

export default {
    getRecipes(perPage, page) {
        return apiClient.get('/getlist/_limit=' + perPage +'/_page=' + page)
    },
    getRecipe(id) {
        return apiClient.get('/getone/' + id)
    },
    postRecipe(newrecipe) {
       return apiClient.post('/newrecipe', newrecipe)
    },
    editRecipe(id, editrecipe) {
        return apiClient.put('/editrecipe/' + id , editrecipe)
    },
    deleteRecipe(id) {
        return apiClient.delete('/deleterecipe/' + id)
    }
}