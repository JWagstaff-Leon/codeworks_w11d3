<template>
    <div class="profile-card d-flex flex-column position-relative selectable" @click="setActive">
        <img :src="profile.picture" class="profile-img my-auto h-75">
        <h6 class="text-center py-2 no-select">{{profile.name}}</h6>
    </div>
</template>

<script>
import { AppState } from '../AppState.js'
export default
{
    props:
    {
        profile:
        {
            type: Object,
            required: true
        }
    },

    setup(props)
    {
        return {
            setActive()
            {
                AppState.activeProfile = props.profile;
            }
        }
    }
}
</script>

<style lang="scss" scoped>
.profile-card
{
    height: 30vh;
    border: 2px solid rgba(0, 0, 0, 0.5)
}

.profile-img
{
    object-fit: cover;
}
</style>