const LOCALHOST = "localhost";
const domain = "localhost";
const apiPort = "5003";
const protocol = "http";
//const domain = "ec2-13-52-181-69.us-west-1.compute.amazonaws.com";
//const apiPort = "8080";
let root = `${protocol}://${domain}/`;
let apiRoot = `${protocol}://${domain}:${apiPort}/api/`
import axios from 'axios'

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
    LOCALHOST: LOCALHOST,
    domain: domain,
    root: root,
    apiRoot: apiRoot,
    api: {
        admin: {
            getUsers: apiRoot + "userManagement/getUsers",
            getRoles: apiRoot + "userManagement/getRoles",
            createUser: apiRoot + "userManagement/createUser",
            updateUsers: apiRoot + "userManagement/updateUsers",
            deleteUsers: apiRoot + "userManagement/deleteUsers",
            runBulkOperation: apiRoot + "userManagement/runBulkOperation"
        },

        userPrivacy: {
            getOptions: apiRoot + "userPrivacy/getOptions",
            setOptions: apiRoot + "userPrivacy/setOptions",
            accountDeletion: apiRoot + "userPrivacy/accountDeletion"
        },

        scheduleBuilder: {
            getList: apiRoot + "schedule/getlist",
            getSchedule: apiRoot + "schedule/getschedule",
            newSchedule: apiRoot + "schedule/newschedule",
            saveSchedule: apiRoot + "schedule/saveschedule",
            createItem: apiRoot + "schedule/createItem",
            updateItem: apiRoot + "schedule/updateItem",
            deleteItem: apiRoot + "schedule/deleteItem",
            getCollaborators: apiRoot + "schedule/getCollaborators",
            addCollaborator: apiRoot + "schedule/addCollaborator",
            updateCollaborator: apiRoot + "schedule/updateCollaborator",
            deleteCollaborator: apiRoot + "schedule/deleteCollaborator",
            searchUser: apiRoot + "schedule/searchUser"
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
            authenticate: apiRoot + "login/authenticate",
            disable: apiRoot + "login/disable",
            getToken: apiRoot + "login/getToken",
        },

        registration: {
            newRegistration: apiRoot + "registration/newRegistration",
            inputValidation: apiRoot + "registration/validation/",
            emailVerification: apiRoot + "registration/emailVerification"
        },

        studentDiscounts: {
            postEstablishment: apiRoot + "studentdiscounts/postEstablishment",
            postWebsite: apiRoot + "studentdiscounts/postWebsite",
            getEstablishments: apiRoot + "studentdiscounts/getDiscountsEstablishment",
            getWebsites: apiRoot + "studentdiscounts/getDiscountsWebsite",
            getWebDetails: apiRoot + "studentdiscounts/getWebDetails",
            getEstDetails: apiRoot + "studentdiscounts/getEstDetails",
        },

        careerOpportunities: {
            getOpportunities: apiRoot + "careerOpportunities/getOpportunities"
        },

        gpaCalc: {
            calculateGPA: apiRoot + "gpaCalc/calculateGPA"

        },

        gradeCalc: {
            calculateGrade: apiRoot + "gradeCalc/calculateGrade",
            saveGrade: apiRoot + "gradeCalc/saveGrade",
            displayRanking: apiRoot + "gradeCalc/displayRanking"
        },

        uad: {
            mostVisited: apiRoot + "uad/mostVisited",
            topSchool: apiRoot + "uad/topSchool",
            averageDuration: apiRoot + "uad/averageDuration",
            numLogin: apiRoot + "uad/numLogin",
            numMatches: apiRoot + "uad/numMatches",
            numRegister: apiRoot + "uad/numRegister"
        },
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
        return apiClient.post('recipe/editrecipe/' + id , editrecipe)
    },
    deleteRecipe(id) {
        return apiClient.post('recipe/deleterecipe/' + id)
    },
    postResetEmail(resetemail){
        return apiClient.post('recovery/reset', resetemail)
    },
    newPassword(newpass){
        return apiClient.post('recovery/passwordchange', newpass)
    },
    validateUserExist(username, email) {
        return apiClient.get('recovery/validate/'+ username + '/' + email)
    },
    validatePassExist(email, pass, confPass) {
        return apiClient.get('recovery/validatepass/' + email + '/' + pass + '/' + confPass)
    },
    postDisableAccount(disabledemail) {
        return apiClient.post('recovery/disabled', disabledemail)
    },
    actAccount(useractivate) {
        return apiClient.post('recovery/postactivate', useractivate)
    }
   
}

export default URLS


