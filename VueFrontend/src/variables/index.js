import axios from 'axios'


const domain = "localhost";
//const apiPort = "5002";
//let root = `https://${domain}/`;
//let apiRoot = `https://${domain}:${apiPort}/api/`;
const apiPort = "5003";
//const domain = "ec2-13-52-181-69.us-west-1.compute.amazonaws.com";
//const apiPort = "8080";
let root = `http://${domain}/`;
let apiRoot = `http://${domain}:${apiPort}/api/`;


const apiClient = axios.create({
    baseURL: apiRoot,
    //baseURL: 'http://ec2-13-52-181-69.us-west-1.compute.amazonaws.com:8080/api/recipe',
    withCredentials: false,
    headers: {
        Accept: 'application/json',
        'Content-Type': 'application/json'
    }
})

const URLS = {
    domain: domain,
    root: root,
    apiRoot: apiRoot,
    api: {
        scheduleBuilder: {
            getList: apiRoot + "schedule/getlist",
            getSchedule: apiRoot + "schedule/getschedule",
            newSchedule: apiRoot + "schedule/newschedule",
            saveSchedule: apiRoot + "schedule/saveschedule",
            createItem: apiRoot + "schedule/createItem",
            updateItem: apiRoot + "schedule/updateItem",
            deleteItem: apiRoot + "schedule/deleteItem"
        },

        matching: {
            matchActivity: apiRoot + "matching/matchActivity",
            matchTutoring: apiRoot + "matching/matchTutoring",
            displayMatches: apiRoot + "matching/displayMatches",
            updateOptStatus: apiRoot + "matching/updateOptStatus"
        },

        activityProfile: {
            update: apiRoot + "activityProfile/update"
        },

        tutoringProfile: {
            update: apiRoot + "tutoringProfile/update"
        },

        scheduleComparison: {
            getComparison: apiRoot + "schedulecomparison/getcomparison",
        },

        login: {
            validate: apiRoot + "login/validate",
            authenticate: apiRoot + "login/authenticate"
        }
    },
    getRecipes(perPage, page) {
        return apiClient.get('recipe/getlist/_limit=' + perPage +'/_page=' + page)
    },
    getRecipe(id) {
        return apiClient.get('recipe/getone/' + id)
    },
    postRecipe(newrecipe) {
        return apiClient.post('recipe/newrecipe', newrecipe)
    },
    editRecipe(id, editrecipe) {
        return apiClient.put('recipe/editrecipe/' + id , editrecipe)
    },
    deleteRecipe(id) {
        return apiClient.delete('recipe/deleterecipe/' + id)
    },
    postResetEmail(resetemail){
        return apiClient.post('recovery/reset', resetemail)
    },
    newPassword(newpass){
        return apiClient.post('recovery/passwordchange', newpass)
    }
}

export default URLS


