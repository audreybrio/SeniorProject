<template>
    <h2>User Management</h2>
    <div v-if="display">
        very true!
        <label for="fileField">Upload bulk operations file here ( size limit: 1MB )</label>
        <div>
            <form>
                <input type="file" id="fileField" required />
                <button @click="onClear" type="reset">Clear</button>
                <button @click="onExecute" type="submit">Execute</button>
            </form>
        </div>
    </div>
    <div v-else>
        false!
    </div>
</template>

<script lang="js">
    /* eslint-disable */
    import router from '../../router'
    import URLS from '../../variables'
    import axios from 'axios'
    const ADMIN = 'admin'
    export default ({
        data() {
            return {
                user: null,
                role: null,
                file: null
            }
        },
        created() {
            //this.user = this.$router.params.user;
            //this.role = this.$router.params.role;
            this.role = ADMIN;
            if (this.role === ADMIN) {
                this.display = true;
            }
        },
        methods: {
            routeUnauthorized() {
                router.push({ name: "not-authorized" });
            },
            routeSingle() {
                router.push({ name: "UserManagement" });
            },
            routeHome() {
                router.push({ name: "HomePage" });
            },
            onClear() {
                this.file = null;
            },
            upload() {
                this.progress = 0;
            },
            onExecute(file, onUploadProgress) {
                let confirmed = confirm("Are you sure you want to upload and execute?");
                if (confirmed) {
                    let formData = new FormData();
                    formData.append("file", file);
                    axios.post(`${URLS.api.admin.runBulkOperation}`, formData, {
                        headers: {
                            "Content-Type": "multipart/form-data"
                        },
                        onUploadProgress
                    })
                        .then(response => {
                            alert(response)
                        })
                        .catch(e => {
                            alert(e)
                        })
                }
            },
        }
    });
</script>

<style>
    table {
        margin: auto;
    }

    th {
        border-width: 1px;
        border-style: solid;
        border-color: black;
    }

    td {
        border-width: 1px;
        border-style: solid;
        border-color: black;
    }
</style>