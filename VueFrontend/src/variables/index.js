const domain = "localhost";
const apiPort = "5003";
//const domain = "ec2-13-52-181-69.us-west-1.compute.amazonaws.com";
//const apiPort = "8080";
let root = `http://${domain}/`;
let apiRoot = `http://${domain}:${apiPort}/api/`;
const URLS = {
    domain: domain,
    root: root,
    apiRoot: apiRoot,
    api: {
        admin: {
            getUsers: apiRoot + "userManagement/getUsers",
            getRoles: apiRoot + "userManagement/getRoles",
            updateUsers: apiRoot + "userManagement/updateUsers",
            deleteUsers: apiRoot + "userManagement/deleteUsers",
            runBulkOperation: apiRoot + "userManagement/runBulkOperation"
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
            deleteCollaborator: apiRoot + "schedule/deleteCollaborator"
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
            getToken: apiRoot + "login/getToken"
        },

        gpaCalc: {

        }
    }
}

export default URLS